/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./public/**/*.{html,js}"],
  theme: {
    container: {
      padding: "2em"
    },
    extend: {
      colors: {
        "prim": "#ac2c52",
        "on-prim": "#ffffff",
        "prim-cont": "#ffd9de",
        "on-prim-cont": "#3f0016",
        "sec": "#75565b",
        "on-sec": "#ffffff",
        "sec-cont": "#ffd9de",
        "on-sec-cont": "#2b1519",
        "ter": "#7a5832",
        "on-ter": "#ffffff",
        "ter-cont": "#ffdcbb",
        "on-ter-cont": "#2b1700",
        "err": "#ba1a1a",
        "on-err": "#ffffff",
        "err-cont": "#ffdad6",
        "on-err-cont": "#410002",
        "sur": "#fffbff",
        "on-sur": "#201a1b",
        "outline": "#9f8c8e",
        "outline-sur": "#efe0cf",
        "outline-on-sur": "#4f4539",
        "sur-var": "#f3dde0",
        "on-sur-var": "#524345"
      }
    }
  },
  plugins: [],
}
