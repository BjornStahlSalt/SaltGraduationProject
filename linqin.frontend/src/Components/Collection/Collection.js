import React from 'react';
import './Collection.css';
import circle from '../../Images/Circle.svg';
import square from '../../Images/Square.svg';
import triangle from '../../Images/Triangle.svg';


function Collection({ shapes }) {
  return (
    <div>
      <div className="Level__Collection">{ shapes.map((s, i) =>
        <img className={ "Level__Shape--" + s.color } key={ i } src={ circle } alt='A circle' />
      ) }
      </div>
    </div>
  );
}
export default Collection;