import React, { useState, useEffect } from "react";
import Collection from "../Collection/Collection.js";
import SubmitButton from "./SubmitButton";
import "./Level.css";
import Result from "../Result/Result.js";
import DescriptionButton from "./DescriptionButton.js";
import { Button } from "@mui/material";

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
    if (level == null) return;
    const requestOptions = {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        listOfShapes: level.startCollection,
        Query: userInput,
      }),
    };

    fetch("https://linqinapi.azurewebsites.net/api/Inputs", requestOptions)
      .then((response) => response.json())
      .then(setLoading(true))
      .then((response) => {
        if (response.errorMessage === null) {
          checkAnswer(expectedResult, response.listOfShapes);
          setLoading(false);
        } else {
          setQueryResult([]);
          setCompileError(response.errorMessage);
          setLoading(false);
        }
      })
      .catch((error) => console.log(error));
  };

  function isString(val) {
    return typeof val === "string";
  }
  function isStrArray(val) {
    return Array.isArray(val) && val.every(isString);
  }

  const checkAnswer = (expected, result) => {
    if (Array.isArray(result) && isStrArray(result)) {

      const expectedList = expected;
      const resultList = result;

      if (JSON.stringify(expectedList) === JSON.stringify(resultList)) {
        setCompileError("Correct!!");
        localStorage.setItem(level.title, JSON.stringify(true));
      } else {
        setCompileError("Wrong Answer!");
      }
      setQueryResult(resultList);

      return;
    }

    if (Array.isArray(result)) {
      const expectedList = expected.map((s) => ({
        shape: s.shape,
        color: s.color,
        priorityValue: s.priorityValue,
      }));
      const resultList = result.map((s) => ({
        shape: s.shape,
        color: s.color,
        priorityValue: s.priorityValue,
      }));

      if (JSON.stringify(expectedList) === JSON.stringify(resultList)) {
        setCompileError("Correct!!");
        localStorage.setItem(level.title, JSON.stringify(true));
      } else {
        setCompileError("Wrong Answer!");
      }
      setQueryResult(resultList);

      return;
    }

    if (typeof result === "number") {
      if (result == expected) {
        setCompileError("Correct!!");
        localStorage.setItem(level.title, JSON.stringify(true));
      } else {
        setCompileError("Wrong Answer!");
      }
      setQueryResult(result);
      return;
    }

    if (typeof result === "boolean") {
      if (result == expected) {
        setCompileError("Correct!!");
        localStorage.setItem(level.title, JSON.stringify(true));
      } else {
        setCompileError("Wrong Answer!");
      }
      setQueryResult(result);
      return;
    }

    if (typeof result === "object") {
      if (JSON.stringify(result) === JSON.stringify(expected)) {
        setCompileError("Correct!!");
        localStorage.setItem(level.title, JSON.stringify(true));
      } else {
        setCompileError("Wrong Answer!");
      }
      setQueryResult(result);
      return;
    }

    console.log("Could not read response");
  };

  const updateInput = (e) => {
    setUserInput(e.target.value);
    setQueryResult([]);
    setCompileError("");
  };

  useEffect(() => {
    setQueryResult([]);
    setUserInput("");
    setCompileError("");

    if (level) {
      let temp = expectedResult;
      if (Array.isArray(level.expectedString) && level.expectedString) {
        temp = level.expectedString;
      } else if (
        Array.isArray(level.expectedCollection) &&
        level.expectedCollection
      ) {
        temp = level.expectedCollection;
      } else if (level.expectedInt) {
        temp = level.expectedInt;
      } else if (level.expectedBool) {
        temp = level.expectedBool;
      } else if (level.expectedSingle) {
        temp = level.expectedSingle;
      }
      setExpectedResult(temp);
    }
  }, [level]);

  if (level == null) {
    return <div></div>;
  }

  return (
    <div className="Level">
      {/* <PropertyList shapes={ level.startCollection } /> */}
      <h3 className="Level__Title">{level.title} </h3>
      {level.description !== "" ? <DescriptionButton level={level} /> : null}
      <p className="Level__Prompt">{level.prompt}</p>

      <Collection
        shapes={level.startCollection}
        shaded=""
        query=""
        animated=" animated"
      />

      <form className="Level__FormContainer" onSubmit={(e) => handleSubmit(e)}>
        <p className="preInput">shapes.</p>
        <input
          type="text"
          className="Level__Form"
          value={userInput}
          onChange={(e) => updateInput(e)}
        />

        <SubmitButton
          submitAnswer={submitAnswer}
          loading={loading}
          compileError={compileError}
        />
        <div className="Level__CompileError">
          <p className="Level__CompileErrorText">{compileError}</p>
        </div>
      </form>

      {/* <Button onClick={handlePrevClick}>Prev</Button>
        <Button onClick={handleNextClick}>Next</Button> */}

      <div className="Level__ResultContainer">
        <Result result={expectedResult} shaded="shaded" query="" animated="" />
        <Result result={queryResult} shaded="" query="query" animated="" />
      </div>
    </div>
  );
}
export default Level;
