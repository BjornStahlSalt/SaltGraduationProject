import React from 'react';
import './Collection.css';


function Collection({ shapes }) {
  return (
    <div className="Level__Collection">{ shapes.map((s, i) => <p className="Level__Shape" key={ i }>{ s.priorityValue }</p>) } </div>
  );
}
export default Collection;