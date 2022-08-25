import "./Home.css";

function Home(props) {
  const handleClick = () => {
    props.setCurrentLevel(props.levels[0]);
  };

  return (
    <div className="Page__ContentHome">
      <div className="Page__TextContainer">
        <h1 className="Page__Title">Welcome Fellow Saltie!</h1>
        <p className="Page__Text">
          Presenting <span className="Page__EmphText">"LinqinAPI"</span>, a tool
          that visualizes LINQ queries to assist you in your first encounters
          with LINQ on your journey through{" "}
          <span className="Page__EmphText">&lt;/SALT&gt;</span>.{" "}
        </p>
        <h2 className="Page__Subtitle">Query?</h2>
        <p className="Page__Text">
          To know what LINQ is, we first need to know what a query is. A query
          is a question or request for information expressed in a formal way. In
          computer science, a query is basically the same thing, the only
          difference is that the answer or the retrieved information comes from
          a database.
        </p>
        <h2 className="Page__Subtitle">So what exactly is LINQ?</h2>
        <p className="Page__Text">
          LINQ stands for{" "}
          <span className="Page__EmphText">"Language Integrated Query"</span>.
          As the name suggests, LINQ allows you to directly query data using the
          C# language and to query data from different data sources e.g. SQL
          databases, collections, XML documents and more. LINQ comes in two
          syntactical flavors: Query and Method syntax - we will focus on Method
          syntax here.{" "}
        </p>
        <p className="Page__Text"> </p>
        <h2 className="Page__Subtitle">Now what?</h2>
        <p className="Page__Text">
          {" "}
          In each challenge you will be given a{" "}
          <span className="Page__EmphText">"List of Shapes"</span>, a list
          containing objects with various properties like shape and color. Your
          job will be to alter the list of shapes using LINQ queries to meet the
          criterias of each level. Difficulty will increase as you work your way
          through the challenges.
        </p>
      </div>
      <button className="Page__Button" onClick={() => handleClick()}>
        Click here to start your journey!
      </button>
    </div>
  );
}
export default Home;
