import React, { useState, useEffect } from 'react';
import './Collection.css';


function StringCollection({ shapes, shaded, animated }) {

  return (
    <div>
      <div className={ `Level__StringCollection${shaded}` }>{ shapes.map((s, i) =>
        <p key={i}>{s}</p>
      ) }
      </div>
    </div>
  );
}
export default StringCollection;