import React, { useState, useEffect } from 'react';
import ExpectedShape from '../Shape/ExpectedShape';
import Shape from '../Shape/Shape';
import './Collection.css';


function ExpectedCollection({ shapes }) {

  return (
    <div>
      <div className="Level__Collection">{shapes.map((s, i) =>
        <ExpectedShape shape={s} />
        // <img className={ "Level__Shape--" + s.color } key={ i } src={ circle } alt='A circle' />
      )}
      </div>
    </div>
  );
}
export default ExpectedCollection;