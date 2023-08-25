import { Button, Input, Modal, Tree } from "antd";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import styles from "./main.module.scss";
import LeftMenu from './LeftMenu';
import RightContent from "./RightContent";


export const Note = () => {
  const navigate = useNavigate();

  const [selectedFolder, setSelectedFolder] = useState(-1);

  return (
    <div className={styles.main}>
      <div className=" left">
        <div className={"mt-50"}></div>
        <LeftMenu 
          onSelectChange={(id:number) => setSelectedFolder(id)}
        />
      </div>
      <div className="right">
        <div>
          <RightContent 
            SelectFolderId={selectedFolder}
          />
        </div>
      </div>
    </div>
  );
};
