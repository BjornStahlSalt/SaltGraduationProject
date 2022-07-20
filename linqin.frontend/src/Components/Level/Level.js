import React from 'react';
import Collection from '../Collection/Collection.js';


function Level({ level }) {
  if (level === null) {
    return <div></div>;
  }
  return (
    <div>
      <h3>Title</h3>
      <p>Prompt</p>
      <div>
        <Collection shapes={ level.startCollection } />
        <Collection shapes={ level.expectedCollection } />
      </div>
    </div>
  );
}
export default Level;