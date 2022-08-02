import React, { useState, useEffect } from "react";
import Shape from "../Shape/Shape";
import "./Collection.css";

function StringCollection({ shapes, query, shaded }) {
  return (
    <div className={`Level__StringCollection${shaded}${query}`}>
      {shapes.map((s, i) => (
        <p key={i}>{s}</p>
      ))}
    </div>
  );
}
export default StringCollection;
