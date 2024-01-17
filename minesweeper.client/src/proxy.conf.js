const PROXY_CONFIG = [
  {
    context: [
      "/minesweeper",
    ],
    target: "https://localhost:7004",
    secure: false
  }
]

module.exports = PROXY_CONFIG;
