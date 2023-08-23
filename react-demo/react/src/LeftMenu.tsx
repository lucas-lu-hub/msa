import React, { useEffect, useState } from 'react';
import { Button, Input, Modal, Tree } from 'antd';
import type { DataNode, DirectoryTreeProps } from 'antd/es/tree';
import getHeaders from './rtks/serviceApiHelper';

const { DirectoryTree } = Tree;

interface Props {
  onSelectChange: (id:number) => void,
  // data: DataNode[]
}

const App = (props : Props):JSX.Element => {

  const onSelect: DirectoryTreeProps['onSelect'] = (keys, info) => {
    console.log('Trigger Select', keys, info);
    setSelectedFolder(keys[0]);
    props.onSelectChange(keys[0])
  };

  const onExpand: DirectoryTreeProps['onExpand'] = (keys, info) => {
    console.log('Trigger Expand', keys, info);
  };

  
  const [selectedFolder, setSelectedFolder] = useState(-1);

  const [showCreateModal, setShowCreateModal] = useState(false);
  const [showDeleteModal, setShowDeleteModal] = useState(false);

  const [createName, setCreateName] = useState("");
  const handleCreateOK = async() => {
    await createFolder(createName, selectedFolder)
    fetchData();
    setShowCreateModal(false);
    setCreateName("");
  }
  const handleCreateCancel = () => {
    setShowCreateModal(false);
    setCreateName("");
  }

  const handleDeleteCancel = () => {
    setShowDeleteModal(false);
  }

  const handleDeleteOK = async() => {
    await deleteFolder(selectedFolder);
    fetchData();
    setShowDeleteModal(false);
  }

  const createFolder = async (createName:string, selectedFolder:number) => {
    await fetch("http://localhost:8000/Notes/folder/AddFolder", {
      method: "POST",
      headers: getHeaders(),
      body: JSON.stringify({
        name: createName,
        parentId: selectedFolder,
      })
    });
  };

  const deleteFolder = async (id:number) => {
    await fetch(`http://localhost:8000/Notes/folder/deleteFolder?id=${id}`, {
      method: "DELETE",
      headers: getHeaders()
    });
  };

  const mapList: DataNode[] = (items: any[]) => {
    return items.map(item => {
      return {
        title: item.name,
        key: item.id,
        children: mapList(item.children)
      }
    });
  };


  const [data, setData] = useState([]);
  const fetchData = async () => {
    try {
      const response = await fetch('http://localhost:8000/notes/folder/getFolders', {
        method: "GET",
        headers: getHeaders(),
      });
      const jsonData = await response.json();
      setData(mapList(jsonData));
    } catch (error) {
      console.error('Error fetching data:', error);
    }
  };
  useEffect(() => {
    fetchData();
  }, []);

  return (
    <div>
      <DirectoryTree
        defaultExpandAll
        onSelect={onSelect}
        onExpand={onExpand}
        treeData={data}
      />
      
      <Button 
          onClick={() => setShowDeleteModal(true)}
        >删除</Button>
        <Button
          onClick={ () => setShowCreateModal(true)}
        >创建</Button>

        
      <Modal
        title="创建文件夹"
        open={showCreateModal}
        onOk={handleCreateOK}
        onCancel={handleCreateCancel}  
      >
        <label>文件夹名</label>
        <Input value={createName} onChange={(e) => setCreateName(e.target.value)}></Input>
      </Modal>
      <Modal
        title="删除文件夹"
        open={showDeleteModal}
        onOk={handleDeleteOK}
        onCancel={handleDeleteCancel}  
      >
        <label></label>
      </Modal>
    </div>
  );
};

export default App;