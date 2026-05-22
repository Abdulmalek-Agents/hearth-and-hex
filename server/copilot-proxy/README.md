# Claude AI Co-pilot Proxy

Tiny Node/Express server between Unity builds and Anthropic's Messages API.
Keeps your API key safe; enforces per-IP rate limiting.

## Local dev

```bash
cp .env.example .env  # set ANTHROPIC_API_KEY
npm install
npm run dev
```

Listens on `http://localhost:8787`. Unity ClaudeCopilotService default URL matches.

## Production deployment

1. Cloudflare Workers (port the handler)
2. Fly.io: `fly launch` + `fly secrets set ANTHROPIC_API_KEY=...`
3. Railway / Render: connect repo, set env, deploy

## Endpoints

- `POST /v1/messages` → Anthropic passthrough
- `GET /health` → `{ok: true}`

## Cost guardrails

- Default model `claude-sonnet-4-5`; switch to `claude-haiku-4-5` for budget characters.
- Unity caps `max_tokens=512`.
- Per-IP limit: 60 req/min.
