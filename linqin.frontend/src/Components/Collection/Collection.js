import React, { useState, useEffect } from "react";
import Shape from "../Shape/Shape";
import ReactCardFlip from "react-card-flip";
import "./Collection.css";

function Collection({ shapes, shaded, query, animated }) {
  return (
    <div className={`Level__Collection${shaded}${query}`}>
      {shapes.map((s, i) => (
        <Shape key={i} shape={s} shaded={shaded} animated={animated} />
      ))}
    </div>
  );
}
export default Collection;
