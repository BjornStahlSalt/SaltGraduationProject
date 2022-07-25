import React, { useState, useEffect } from 'react';
import Shape from '../Shape/Shape';
import './result.css';


function Rsult({ result, shaded }) {
  const [resultHtml, setResultHtml] = useState('');
  useEffect(() => {

    if (Array.isArray(result)) {
      setResultHtml(
        [<Collection shapes={result} shaded='shaded' />,
        <Collection shapes={queryShapes} shaded='' />]
      );
      return;
    }

    if (typeof result === 'number') {
      setResultHtml(<p>{result}</p>);
      return;
    }

    if (typeof result === 'boolean') {
      console.log('We got a bool');
      return;
    }

    if (typeof result === 'object') {
      console.log('We got an object');
      return;
    }


  }, [result]);

  return (
    <div>
      {html.map((el) => el)}
    </div>
  );
}
export default Result;