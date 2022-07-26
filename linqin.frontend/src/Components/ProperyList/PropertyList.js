import React, { useState, useEffect } from 'react';
import Shape from '../Shape/Shape';
import './PropertyList.css';


function PropertyList({ shapes }) {
  const [distinctShapes, setDistinctShapes] = useState([]);
  useEffect(() => {
    const unique = [...new Map(shapes.map(obj => [JSON.stringify(obj), obj])).values()];
    console.log(unique);

    console.log(unique);
    setDistinctShapes(unique);

  }, [shapes]);

  return (
    <div className='Properties'>{
      distinctShapes.map((s, i) =>
        <div className='Properties__Shape' key={i}>
          <Shape shape={s} shaded='' large='large' />
          <p className='Properties__Property'>.Shape = "{s.shape}"</p>
          <p className='Properties__Property'>.Color = "{s.color}"</p>
        </div>
      )}
    </div>
  );
}
export default PropertyList;