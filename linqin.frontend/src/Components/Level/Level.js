import React, { useState, useEffect } from 'react';
import Collection from '../Collection/Collection.js';
import SubmitButton from './SubmitButton';
import './Level.css';
import Result from '../Result/Result.js';
import DescriptionButton from './DescriptionButton.js';
import { Button } from '@mui/material';


function Level({ level, handleNextClick, handlePrevClick }) {
  const [userInput, setUserInput] = useState("");
  const [compileError, setCompileError] = useState("");
  const [queryResult, setQueryResult] = useState([]);
  const [expectedResult, setExpectedResult] = useState([]);
  const [loading, setLoading] = useState(false);


  const handleSubmit = (e) => {
    e.preventDefault();

    submitAnswer();
  };

  const submitAnswer = () => {
    if (level == null)
      return;
    console.log(level.description);
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

    fetch('https://linqinapi.azurewebsites.net/api/Inputs', requestOptions)
      .then(response => response.json())
      .then(setLoading(true))
      .then(response => {
        if (response.errorMessage === null) {
          console.log(response.listOfShapes);
          checkAnswer(expectedResult, response.listOfShapes);
          setLoading(false);
        }
        else {
          setQueryResult([]);
          setCompileError(response.errorMessage);
          setLoading(false);
        }
      })
      .catch(error => console.log(error));
  };

  function isString(val) {
    return (typeof val === "string");
  }
  function isStrArray(val) {
    return Array.isArray(val) && val.every(isString);
  }

  const checkAnswer = (expected, result) => {
    if (Array.isArray(result) && isStrArray(result)) {
      console.log('We got an array of strings');

      const expectedList = expected;
      const resultList =  result;
      console.log(JSON.stringify(expectedList));
      console.log(JSON.stringify(resultList));

      if (JSON.stringify(expectedList) === JSON.stringify(resultList)) {
        console.log("correct");
        console.log("wrong");
        setCompileError('Correct!!');
        localStorage.setItem(level.title, JSON.stringify(true));
      }
      else {
        console.log("correct2");
        console.log("wrong2");
        setCompileError('Wrong Answer!');
      }
      setQueryResult(resultList);

      return;
    }

    if (Array.isArray(result)) {
      console.log('We got an array of objects');

      const expectedList = expected.map(s => ({ shape: s.shape, color: s.color, priorityValue: s.priorityValue }));
      const resultList = result.map(s => ({ shape: s.shape, color: s.color, priorityValue: s.priorityValue }));

      if (JSON.stringify(expectedList) === JSON.stringify(resultList)) {
        setCompileError('Correct!!');
        localStorage.setItem(level.title, JSON.stringify(true));
      }
      else {
        setCompileError('Wrong Answer!');
      }
      setQueryResult(resultList);

      return;
    }

    if (typeof result === 'number') {
      console.log('We got a number');
      setQueryResult(result);
      return;
    }

    if (typeof result === 'boolean') {
      console.log('We got a bool');
      setQueryResult(result);
      return;
    }

    if (typeof result === 'object') {
      console.log('We got an object');
      setQueryResult(result);
      return;
    }

    console.log('Could not read response');
  };

  const updateInput = (e) => {
    setUserInput(e.target.value);
    setQueryResult([]);
    setCompileError('');
  };

  useEffect(() => {
    setQueryResult([]);
    setUserInput('');
    setCompileError('');

    if (level) {
      console.log(level.expectedString);

      let temp = expectedResult;
      if (Array.isArray(level.expectedString) && level.expectedString) {
        console.log('expected array of strings');
        temp = level.expectedString;
      }
      else if (Array.isArray(level.expectedCollection) && level.expectedCollection) {
        console.log('expected array of objects');
        temp = level.expectedCollection;
      }
      else if (level.expectedInt) {
        temp = level.expectedInt;
      }
      else if (level.expectedBool) {
        temp = level.expectedBool;
      }
      else if (level.expectedSingle) {
        console.log('expected object');
        temp = level.expectedSingle;
      }
      setExpectedResult(temp);
    }


  }, [level]);

  if (level == null) {
    return (<div></div>);
  }

  return (
    <div className='Level'>
      {/* <PropertyList shapes={ level.startCollection } /> */}
      <h3 className='Level__Title'>{level.title} {level.description !== '' ? <DescriptionButton level={level} /> : null}</h3>
      <p className='Level__Prompt'>{level.prompt}</p>
      <div className='Level__Content'>
        <div>
          <Collection shapes={level.startCollection} shaded='' animated=' animated' />
        </div>
        <form className='Level__InputBit' onSubmit={e => handleSubmit(e)}>
          <p className="preInput">shapes.</p>
          <input type='text' className="Level__InputForm" value={userInput} onChange={e => updateInput(e)} />
        </form>
        <p>{ compileError }</p>
        <SubmitButton className="Level__Button--Submit" submitAnswer={ submitAnswer } loading={ loading } compileError={ compileError } />
        <Button onClick={ handlePrevClick }>Prev</Button>
        <Button onClick={ handleNextClick }>Next</Button>
      </div>
      <Result result={expectedResult} shaded='shaded' animated='' />
      <Result result={queryResult} shaded='' animated='' />
    </div>
  );
}
export default Level;