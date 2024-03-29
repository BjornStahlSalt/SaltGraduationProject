import React, { useState, useEffect } from "react";
import "./Shape.css";
import circle from "../../Images/Circle.svg";
import square from "../../Images/Square.svg";
import ReactCardFlip from "react-card-flip";
import triangle from "../../Images/Triangle.svg";

function Shape({ shape, shaded, animated }) {
  const chooseImage = () => {
    if (shape.shape === "Circle") {
      return circle;
    }
    if (shape.shape === "Square") {
      return square;
    }
    if (shape.shape === "Triangle") {
      return triangle;
    }
    return "";
  };

  if (animated === "") {
    return (
      <img
        className={`Level__Shape--${shape.color} ${shaded}`}
        src={chooseImage(shape)}
        alt={`A '${shape.Shape}`}
      />
    );
  }

  return (
    <div className={"Container"}>
      <div className={`Card${animated}`}>
        <div className={`Face Front${animated}`}>
          <img
            className={`Level__Shape--${shape.color} ${shaded}`}
            src={chooseImage(shape)}
            alt={`A '${shape.Shape}`}
          />
        </div>
        <div className="Face Back">
          <img
            className={`Level__Shape--${shape.color} large`}
            src={chooseImage(shape)}
            alt={`A '${shape.Shape}`}
          />
          <p className="Properties__Property">.Shape = "{shape.shape}"</p>
          <p className="Properties__Property">.Color = "{shape.color}"</p>
        </div>
      </div>
    </div>
  );
}
export default Shape;
