import Image from "next/image";
import tailwindConfig from "../tailwind.config";
import resolveConfig from "tailwindcss/resolveConfig";

export default function Home() {
  const theme = require("../public/theme.json");

  return (
    <>
      {Object.getOwnPropertyNames(theme).map((n) => {
        {
          return (
            <div
              className={`w-10% h-10%`}
              style={{ backgroundColor: n }}
              onClick={() => {
                Object.getOwnPropertyNames(theme[n]).map((p) => {
                  theme[n][p].map((x, index) => {
                    document.documentElement.style.setProperty(
                      `--${p}-${index}`,
                      x
                    );
                  });
                });
              }}
            ></div>
          );
        }
      })}
      <div className="w-100% h-20% bg-back"></div>
      <div className="w-100% h-20% bg-back-0"></div>
    </>
  );
}
