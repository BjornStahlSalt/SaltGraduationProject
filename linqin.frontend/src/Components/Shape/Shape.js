import React, { useState, useEffect } from 'react';
import './Shape.css';
import circle from '../../Images/Circle.svg';
import square from '../../Images/Square.svg';
import triangle from '../../Images/Triangle.svg';
import circleLarge from '../../Images/Circle_Large.svg';
import squareLarge from '../../Images/Square_Large.svg';
import triangleLarge from '../../Images/Triangle_Large.svg';


function Shape({ shape, shaded, large }) {

  const chooseImage = () => {
    if (shape.shape === 'Circle') {
      return circle;
    }
    if (shape.shape === 'Square') {
      return square;
    }
    if (shape.shape === 'Triangle') {
      return triangle;
    }
    return '';

  };

  return (
    <>
      <img className={`Level__Shape--${shape.color} ${shaded} ${large}`} src={chooseImage(shape)} alt='A circle' />
    </>
  );
}
export default Shape;