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
  const [state, setState] = useState({
    type: ViewType.Login,
    userName: '',
    password: ''
  });

  const getToken = async (state: { type: number; userName: string; password: string; }) => {
    const ret = await fetch("https://localhost:8000/Identities/account/login", {
      method: "POST",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      body: JSON.stringify(state)
    });
    await ret.json().then((r) => {
      console.log(r);
      window.localStorage.setItem("lucasNote.Token", r);
      navigate("/note");
    });
  };

  const regist = async (state: { type: number; userName: string; password: string; }) => {
    await fetch("https://localhost:8000/Users/user/Add", {
      method: "POST",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      body: JSON.stringify({...state, userId: 0, gender: 1, email: ''})
    });

  }

  const changType = (t: number) => {
    setState({...state, type: t});
  }

  const setName = (n: string) => {
    setState({...state, userName: n});
  }

  
  const setPwd = (p: string) => {
    setState({...state, password: p});
  }

  return (
    <div>
      <div className={"p-4 w-80 h-80 m-auto mt-52 border-2 bg-[#dedeee3f]"}>
        <div className={styles.AltHei}></div>
        <div className={styles.AltHei}></div>
        <Input type="text" placeholder="用户名" onChange={(e) => setName(e.target.value.trim())}></Input>
        <div className={styles.AltHei}></div>
        <Input type="password" placeholder="密码" onChange={(e) => setPwd(e.target.value.trim())}></Input>
        <div className={styles.AltHei}></div>
        <div className={styles.AltHei}></div>
        <div className={styles.AltHei}></div>
        <Button className="ml-10" 
          onClick={() => changType(state.type === ViewType.Login ? ViewType.Regist : ViewType.Login)}
        >
          {state.type == ViewType.Login ? '去注册' : '去登陆'}
        </Button>
        <Button className="ml-10" onClick={ () => {
          return state.type == ViewType.Login ? getToken(state) : regist(state);
        }}>{state.type === ViewType.Login ? '登录' : '注册'}</Button>
      </div>
    </div>
  );
};
