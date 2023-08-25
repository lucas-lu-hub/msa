import { Button, Input, Modal, Tabs } from "antd";
import { useEffect, useState } from "react";
import ReactQuill from 'react-quill'
import getHeaders from "./rtks/serviceApiHelper";
import 'react-quill/dist/quill.snow.css';

interface Props {
  SelectFolderId: number
}

const RightContent = (props : Props):JSX.Element => {

  const [head, setHead] = useState("");
  const [content, setContent] = useState("");

  const fetchNotes = async (folderId:number): Promise<Note[]> => {
    if ( folderId <= 0){
      return [];
    }
    try {
      const response = await fetch(`http://localhost:8000/notes/note/getNoteByFolderId?folderId=${folderId}`, {
        method: "GET",
        headers: getHeaders(),
      });
      return await response.json();
    } catch (error) {
      console.error('Error fetching data:', error);
    }
    return [];
  };

  const fetchNoteById = async (id:string): Promise<Note | null> => {
    if (id == "-1"){
      return null;
    }
    try {
      const response = await fetch(`http://localhost:8000/notes/note/getNoteById?id=${id}`, {
        method: "GET",
        headers: getHeaders(),
      });
      return await response.json();
    } catch (error) {
      console.error('Error fetching data:', error);
    }
    return null;
  };

  const deleteNote = async (id:string): Promise<Note | null> => {
    if (id == "-1"){
      return null;
    }
    try {
      await fetch(`http://localhost:8000/notes/note/DeleteNotes`, {
        method: "DELETE",
        headers: getHeaders(),
        body: JSON.stringify([id])
      });
    } catch (error) {
      console.error('Error fetching data:', error);
    }
    return null;
  };

  const defaultData: {key: number, label: string}[] = []
  const [data, setData] = useState(defaultData);
  const [selectedTabKey, setSelectedTabKey] = useState("-1");

  const onChange = (key: string) => {
    setSelectedTabKey(key)
  }

  useEffect(() => {
    const notes = fetchNotes(props.SelectFolderId);
    notes.then((data) => {
      const aaa = data.map(item => {
        return {
          key: item.id,
          label: item.name
        }
      })
      setData(aaa);
    })
  }, [props.SelectFolderId])

  useEffect(() => {
    const note = fetchNoteById(selectedTabKey);
    note.then((n) => {
      setHead(n?.name ?? "");
      setContent(n?.content ?? "");
    })
  }, [selectedTabKey])


  const [showDelete, setShowDelete] = useState(false);

  const handleDeleteOK = () => {
    deleteNote(selectedTabKey);
    setShowDelete(false);
  }
  const handleDeleteCancel = () => {
    setShowDelete(false);
  }

  const saveNote = async(id: string, name: string, content: string, folderId: number) => {
    try {
      const response = await fetch(`http://localhost:8000/notes/note/UpdateNote`, {
        method: "POST",
        headers: getHeaders(),
        body: JSON.stringify({
          id: id,
          name: name,
          folderId: folderId,
          content: content,
          tag: ""
        })
      });
        return await response.json();
    } catch (error) {
      console.error('Error fetching data:', error);
    }
    return null;
  }

  return (<div>
    {props.SelectFolderId > 0 && (<div>
    <div className="p-4"></div>
      
      <Button onClick={() => {
        setData([...data, {
          key: -1,
          label: "临时name"
        }]);
        setSelectedTabKey("-1");
      }}>
        创建日记
      </Button>
      {selectedTabKey !== '-1' && (<Button onClick={() => setShowDelete(true)}>删除日记</Button>)}
      {selectedTabKey !== '-1' && (<Tabs 
        activeKey={selectedTabKey}
        items={data}
        onChange={onChange}
      />)}
      {selectedTabKey !== '-1' && (<Input 
        className="" 
        placeholder="再次输入header" 
        value={head} 
        onChange={(e) => {
          setHead(e.target.value);
        }} 
      />)}
      {selectedTabKey !== '-1' && (<ReactQuill 
        className="h-[500px]"
        value={content}
        theme="snow"
        onChange={setContent}
      />)}
      {selectedTabKey !== '-1' && (<Button onClick={() => {
        saveNote(selectedTabKey, head, content, props.SelectFolderId);
        const data = fetchNotes(props.SelectFolderId);
        data.then((d) => {      
          const aaa = d.map(item => {
            return {
              key: item.id,
              label: item.name
            }
        })
        setData(aaa);
        })
      }}>保存</Button>)}
    </div>)}
    <Modal
      title="删除文件夹"
      open={showDelete}
      onOk={handleDeleteOK}
      onCancel={handleDeleteCancel}  
    >
    </Modal>
  </div>)
}

interface Note {
  id: number,
  name: string,
  content: string
}

export default RightContent;