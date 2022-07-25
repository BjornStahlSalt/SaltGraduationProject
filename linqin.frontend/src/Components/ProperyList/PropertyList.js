import React, { useState, useEffect } from 'react';
import Shape from '../Shape/Shape';
import './PropertyList.css';


function PropertyList({ shapes }) {
  const [distinctShapes, setDistinctShapes] = useState([]);
  useEffect(() => {
    // const distShapes = [... new Set(shapes)];
    // const test = shapes.map(s => s.color);
    const unique = [...new Map(shapes.map(obj => [JSON.stringify(obj), obj])).values()];
    // shapes.forEach(s => unique.add(s));

    // const onlyUnique = (value, index, self) => {
    //   return self.indexOf(value) === index;
    // }

    // const distShapes = test.filter(onlyUnique);
    console.log(unique);
    setDistinctShapes(unique);

  }, [shapes]);

  return (
    <div>
      <div className='Level__Properties'>{
        distinctShapes.forEach((s, i) =>
          <div key={i}>
            <Shape shape={s} shaded='' />
            <p>.Shape = "{s.shape}"</p>
            <p>.Color = "{s.color}"</p>
            {/* <p>{s.Color}</p> */}
          </div>
        )}
      </div>
    </div>
  );
}
export default PropertyList;