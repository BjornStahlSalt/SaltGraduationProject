import React, { useState, useEffect } from 'react';
import Shape from '../Shape/Shape';
import './Collection.css';


function Collection({ shapes }) {

  return (
    <div>
      <div className="Level__Collection">{shapes.map((s, i) =>
        <Shape shape={s} />
        // <img className={ "Level__Shape--" + s.color } key={ i } src={ circle } alt='A circle' />
      )}
      </div>
    </div>
  );
}
export default Collection;