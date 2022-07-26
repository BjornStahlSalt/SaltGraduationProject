import React, { useState, useEffect } from 'react';
import Shape from '../Shape/Shape';
import './Collection.css';


function Collection({ shapes, shaded }) {

  return (
    <div>
      <div className={`Level__Collection${shaded}`}>{shapes.map((s, i) =>
        <Shape key={i} shape={s} shaded={shaded} />
      )}
      </div>
    </div>
  );
}
export default Collection;