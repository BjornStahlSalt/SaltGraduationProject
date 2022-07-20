import React from 'react';
import './Collection.css';
import circle from '../../Images/Circle.svg'


function Collection({ shapes }) {
  return (
    <div>
      <div className="Level__Collection">{shapes.map((s, i) =>
        <img className="Level__Shape" key={i} src={circle} alt='A circle' />
      )}
      </div>
    </div>
  );
}
export default Collection;