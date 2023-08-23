import { Button, Input, Modal, Tree } from "antd";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import styles from "./main.module.scss";
import LeftMenu from './LeftMenu';


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
        <div className="header font-bold">header</div>
        <div>
          
        </div>
      </div>
    </div>
  );
};
