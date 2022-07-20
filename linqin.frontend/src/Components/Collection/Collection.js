import React from 'react';


function Collection({ shapes }) {
  return (
    <div>{shapes.map((s, i) => <p key={i}>{s.priorityValue}</p>)} </div>
  );
}
export default Collection;