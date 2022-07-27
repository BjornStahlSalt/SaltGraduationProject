import React, { useState, useEffect } from 'react';
import Shape from '../Shape/Shape';
import ReactCardFlip from 'react-card-flip';
import './Collection.css';


function Collection({ shapes, shaded, animated }) {

  return (
    <div>
      <div className={ `Level__Collection${shaded}` }>{ shapes.map((s, i) =>
        <Shape key={ i } shape={ s } shaded={ shaded } animated={ animated } />
      ) }
      </div>
    </div>
  );
}
export default Collection;