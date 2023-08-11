import { Button, Modal } from "antd";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import styles from "./main.module.scss";

export const Note = () => {
  const navigate = useNavigate();
  const [showModal, setShowModal] = useState(false);

  const requestData = async () => {
    const ret = await fetch("https://localhost:7189/WeatherForecast", {
      // method: "GET",
      // headers: {
      //   Accept: "application/json",
      //   "Content-Type": "application/json",
      // },
    });

    await ret.json().then((r) => {
      console.log(r);
    });
  };

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
