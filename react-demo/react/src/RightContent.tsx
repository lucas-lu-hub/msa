import { Divider } from "antd";
import { useState } from "react";

interface Props {
  SelectFolderId: number
}

const App = (props : Props):JSX.Element => {

  const fetchNotes = (folderId:number) => {
    console.log("");
    setData([]);
    return [];
  }

  const [data, setData] = useState([]);

  // const fetchContent = (noteId:number) => {
  //   console.log("");
  // }


  return (<div>
    <div>
      {data.map(d => return)}
    </div>
  </div>)
}

interface Note {
  id: number,
  name: string,
  content: string
}

export default App;