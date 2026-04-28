# TicTacToeDemo ♟️

A small end-to-end **.NET** demo that shows how to build a simple domain feature (Tic-Tac-Toe) with:

- ✅ Clear domain rules (**invariants** + **state machine**)
- ✅ Unit tests (**NUnit**)
- ✅ ASP.NET Core **Web API** + **Razor UI** (JavaScript `fetch`)
- 🚧 Persistence with **EF Core + SQL** + **Optimistic Concurrency (RowVersion)** *(roadmap)*

---

## ✨ What you can do

- Create a new game
- Play from the browser (3×3 board)
- See status updates (InProgress / Won / Draw)
- Invalid moves are rejected (turn order, out-of-bounds, occupied cell)
- Moves are rejected after the game is finished

---

## 🧱 Solution structure

| Project | Purpose |
|--------|---------|
| **TicTacToe.Domain** | Domain contracts + rules (enums/records/interfaces, invariants) |
| **TicTacToe.Tests** | NUnit tests (turn order, bounds, occupied, win, draw, after finish) |
| **TicTacToe.Web** | Razor UI + Web API endpoints (`/games`, `/moves`) |
| **TicTacToe.Infrastructure** | EF Core DbContext + Entities + Migrations *(in progress / next)* |

---

## 🚀 Run locally

### Prerequisites
- **.NET SDK 7+**

### Start the app
```bash
dotnet build
dotnet run --project TicTacToe.Web