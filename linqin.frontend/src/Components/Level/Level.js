import React, { useState, useEffect } from 'react';
import Collection from '../Collection/Collection.js';
import ExpectedCollection from '../Collection/ExpectedCollection.js';
import './Level.css';


function Level({ level }) {
  const [userInput, setUserInput] = useState("test.OrderBy(s=>s.PriorityValue);");
  const [compileError, setCompileError] = useState("");
  const [queryShapes, setQueryShapes] = useState([]);



  useEffect(() => {
    if (level == null)
      return;

    const requestOptions = {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        listOfShapes: level.startCollection,
        Query: userInput
      }),
    };

    fetch('https://localhost:7003/api/Inputs', requestOptions)
      .then(response => response.json())
      .then(response => {
        if (response.errorMessage === null) {
          setQueryShapes(response.listOfShapes);
          setCompileError('Awsome!');
        }
        else {
          setQueryShapes([]);
          setCompileError('You got an error : ' + response.errorMessage);
        }
      })
      .catch(error => console.log(error));
  }, [userInput, level]);


  if (level == null) {
    return (<div></div>);
  }

  return (
    <div className='Level'>
      <h3 className='Level__Title'>{level.title}</h3>
      <p>Prompt</p>
      <div>
        <Collection shapes={level.startCollection} />
        <ExpectedCollection shapes={level.expectedCollection} />
      </div>
      <input type='text' className="Level__InputForm" value={userInput} onChange={e => setUserInput(e.target.value)} />
      <p>{compileError}</p>
      <Collection shapes={queryShapes} />
    </div>
  );
}
export default Level;