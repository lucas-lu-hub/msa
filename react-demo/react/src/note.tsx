import { Button, Modal } from "antd";
import MenuItem from "antd/es/menu/MenuItem";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import styles from "./main.module.scss";

export const Note = () => {
  const navigate = useNavigate();
  const [showModal, setShowModal] = useState(false);

  return (
    <div className={styles.main}>
      <div className="left">left</div>
      <div className="right">
        <div className="header font-bold">header</div>
        <div>
          <Button
            onClick={() => {
              navigate(-1);
            }}
          >
            back
          </Button>

          <Button onClick={requestData}>request</Button>
          <Button
            onClick={() => {
              setShowModal(true);
            }}
          >
            open modal
          </Button>
          {showModal && (
            <Modal
              open
              onCancel={() => {
                setShowModal(false);
              }}
            >
              <div>hello</div>
            </Modal>
          )}
        </div>
      </div>
    </div>
  );
};
