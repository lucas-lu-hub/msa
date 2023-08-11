import { Button, Input } from "antd";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import styles from "./main.module.scss";

const ViewType = {
  Login: 1,
  Regist: 2
}

export const Login = () => {
  const navigate = useNavigate();
  const [type, setType] = useState(ViewType.Login);

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
      navigate("/note");
    });
  };

  const changType = (t: number) => {
    setType(t);
  }

  return (
    <div>
      <div className={"p-4 w-80 h-80 m-auto mt-52 border-2 bg-[#dedeee3f]"}>
        <div className={styles.AltHei}></div>
        <div className={styles.AltHei}></div>
        <Input type="text" placeholder="用户名"></Input>
        <div className={styles.AltHei}></div>
        <Input type="password" placeholder="密码："></Input>
        <div className={styles.AltHei}></div>
        <div className={styles.AltHei}></div>
        <div className={styles.AltHei}></div>
        <Button className="ml-10" 
          onChange={() => changType(type)}>
          {type == ViewType.Login ? '去注册' : ''}
        </Button>
        <Button className="ml-10" onClick={requestData}>登录</Button>
      </div>
    </div>
  );
};
