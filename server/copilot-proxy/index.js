// Inventix Games — Claude AI Co-pilot Proxy
// Forwards Unity HTTP requests to Anthropic's Messages API.
// Keeps ANTHROPIC_API_KEY off the client. Per-IP rate-limited.

const express = require('express');
const fetch = require('node-fetch');

const app = express();
app.use(express.json({ limit: '256kb' }));

// Naive per-IP rate limiting (replace with Redis in production).
const buckets = new Map();
const LIMIT = 60;
const WINDOW_MS = 60_000;

app.use((req, res, next) => {
    const ip = req.headers['x-forwarded-for'] || req.socket.remoteAddress;
    const now = Date.now();
    const arr = (buckets.get(ip) || []).filter(t => now - t < WINDOW_MS);
    if (arr.length >= LIMIT) return res.status(429).json({ error: 'rate_limited' });
    arr.push(now);
    buckets.set(ip, arr);
    next();
});

app.post('/v1/messages', async (req, res) => {
    const apiKey = process.env.ANTHROPIC_API_KEY;
    if (!apiKey) return res.status(500).json({ error: 'ANTHROPIC_API_KEY not set' });
    try {
        const r = await fetch('https://api.anthropic.com/v1/messages', {
            method: 'POST',
            headers: {
                'x-api-key': apiKey,
                'anthropic-version': '2023-06-01',
                'content-type': 'application/json',
            },
            body: JSON.stringify(req.body),
        });
        const data = await r.json();
        res.status(r.status).json(data);
    } catch (err) {
        console.error(err);
        res.status(502).json({ error: 'upstream_failure', detail: err.message });
    }
});

app.get('/health', (_, res) => res.json({ ok: true }));

const PORT = process.env.PORT || 8787;
app.listen(PORT, () => console.log(`[copilot-proxy] listening on :${PORT}`));
