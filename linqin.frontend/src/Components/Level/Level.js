import React, { useState, useEffect } from 'react';
import Collection from '../Collection/Collection.js';


function Level({ level }) {
  const [userInput, setUserInput] = useState("");
  const [compileError, setCompileError] = useState("");
  const [queryShapes, setQueryShapes] = useState("");



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
      .then(response => {if(response.errorMessage === '')
      {
        setQueryShapes(response.listOfShapes)
      }
      else
      {
        setCompileError(response.errorMessage)
      }
    })
      .catch(error => console.log(error));
  }, [userInput, level]);


  if (level == null) {
    return (<div></div>);
  }

  return (
    <div>
      <h3>Title</h3>
      <p>Prompt</p>
      <div>
        <Collection shapes={level.startCollection} />
        <Collection shapes={level.expectedCollection} />
      </div>
      <input type='text' value={userInput} onChange={e => setUserInput(e.target.value)} />
      <p>You got an error : {compileError}</p>
    </div>
  );
}
export default Level;