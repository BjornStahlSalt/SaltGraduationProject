import React from 'react';


function Level({ level }) {

  return (
    <div>
    <h3>Title</h3>
    <p>Prompt</p>
    <div>
      <Collection shapes={currentLevel.startCollection} />
      <Collection shapes={currentLevel.expectedCollection} />
    </div>
  </div>
  );
}
export default Level;