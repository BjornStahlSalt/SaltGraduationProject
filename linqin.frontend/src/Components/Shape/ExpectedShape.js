import React, { useState, useEffect } from 'react';
import './Shape.css';
import circle from '../../Images/Circle.svg';
import square from '../../Images/Square.svg';
import triangle from '../../Images/Triangle.svg';


function ExpectedShape({ shape }) {
  // const [image, setImage] = useState([]);

  // useEffect(() => {
  // if (shape.shape === 'Circle') {
  //   setImage(circle);
  // }
  // if (shape.shape === 'Square') {
  //   setImage(square);
  // }
  // if (shape.shape === 'Triangle') {
  //   setImage(triangle);

  // setImage('<img className={"Level__Shape--" + shape.color} src={image} alt=\'A circle\' />');
  // }
  // }, []);

  const chooseImage = () => {
    if (shape.shape === 'Circle') {
      return <img className={"Level__Shape--" + shape.color + " shaded"} src={circle} alt='A circle' />;
    }
    if (shape.shape === 'Square') {
      return <img className={"Level__Shape--" + shape.color + " shaded"} src={square} alt='A circle' />;
    }
    if (shape.shape === 'Triangle') {
      return <img className={"Level__Shape--" + shape.color + " shaded"} src={triangle} alt='A circle' />;
    }
    return <img className={"Level__Shape" + shape.color} src='' alt={shape.shape} />;

  };

  return (
    <>
      {chooseImage()}
    </>
    // <img className={"Level__Shape--" + shape.color} src={() => chooseImage(shape)} alt='A circle' />
  );
}
export default ExpectedShape;