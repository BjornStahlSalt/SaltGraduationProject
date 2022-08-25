import React, { useState, useEffect } from "react";
import Collection from "../Collection/Collection.js";
import StringCollection from "../Collection/StringCollection.js";
import Shape from "../Shape/Shape";
import "./Result.css";

function isString(val) {
  return typeof val === "string";
}
function isStrArray(val) {
  return Array.isArray(val) && val.every(isString);
}

function Result({ result, shaded, query, animated }) {
  const [resultHtml, setResultHtml] = useState("");

  useEffect(() => {
    console.log(result);
    if (isStrArray(result)) {
      setResultHtml(
        <StringCollection
          shapes={result}
          query={query}
          shaded={shaded}
          animated={animated}
        />
      );
      return;
    }

    if (Array.isArray(result)) {
      setResultHtml(
        <Collection
          shapes={result}
          query={query}
          shaded={shaded}
          animated={animated}
        />
      );
      return;
    }

    if (typeof result === "number") {
      setResultHtml(<p className="Result__Text">{result}</p>);
      return;
    }

    if (typeof result === "boolean") {
      setResultHtml(<p className="Result__Text">{result.toString()}</p>);
      return;
    }

    if (typeof result === "object") {
      console.log("We got an object");
      setResultHtml(
        <Shape
          shape={result}
          query={query}
          shaded={shaded}
          animated={animated}
          large=""
        />
      );
      return;
    }
  }, [result]);

  return <div className={`Result${shaded}${query}`}>{resultHtml}</div>;
}
export default Result;
