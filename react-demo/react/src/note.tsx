import { Button, Modal, Tree } from "antd";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import styles from "./main.module.scss";
import LeftMenu from './LeftMenu';

// const { DirectoryTree } = Tree;

// const LeftMenu = () => {
//   const treeData: DataNode[] = [
//     {
//       title: 'parent 1',
//       key: '0-0',
//       icon: <CarryOutOutlined />,
//       children: [
//         {
//           title: 'parent 1-0',
//           key: '0-0-0',
//           icon: <CarryOutOutlined />,
//           children: [
//             { title: 'leaf', key: '0-0-0-0', icon: <CarryOutOutlined /> },
//             {
//               title: (
//                 <>
//                   <div>multiple line title</div>
//                   <div>multiple line title</div>
//                 </>
//               ),
//               key: '0-0-0-1',
//               icon: <CarryOutOutlined />,
//             },
//             { title: 'leaf', key: '0-0-0-2', icon: <CarryOutOutlined /> },
//           ],
//         },
//         {
//           title: 'parent 1-1',
//           key: '0-0-1',
//           icon: <CarryOutOutlined />,
//           children: [{ title: 'leaf', key: '0-0-1-0', icon: <CarryOutOutlined /> }],
//         },
//         {
//           title: 'parent 1-2',
//           key: '0-0-2',
//           icon: <CarryOutOutlined />,
//           children: [
//             { title: 'leaf', key: '0-0-2-0', icon: <CarryOutOutlined /> },
//             {
//               title: 'leaf',
//               key: '0-0-2-1',
//               icon: <CarryOutOutlined />,
//               switcherIcon: <FormOutlined />,
//             },
//           ],
//         },
//       ],
//     },
//     {
//       title: 'parent 2',
//       key: '0-1',
//       icon: <CarryOutOutlined />,
//       children: [
//         {
//           title: 'parent 2-0',
//           key: '0-1-0',
//           icon: <CarryOutOutlined />,
//           children: [
//             { title: 'leaf', key: '0-1-0-0', icon: <CarryOutOutlined /> },
//             { title: 'leaf', key: '0-1-0-1', icon: <CarryOutOutlined /> },
//           ],
//         },
//       ],
//     },
//   ];
//   // const getToken = async (state: { type: number; userName: string; password: string; }) => {
//   //   const ret = await fetch("https://localhost:8000/Identities/account/login", {
//   //     method: "POST",
//   //     headers: {
//   //       Accept: "application/json",
//   //       "Content-Type": "application/json",
//   //     },
//   //     body: JSON.stringify(state)
//   //   });
//   //   await ret.json().then((r) => {
//   //     console.log(r);
//   //     window.localStorage.setItem("lucasNote.Token", r);
//   //     navigate("/note");
//   //   });
//   // };

//   return <DirectoryTree
//   showLine={false}
//   showIcon={false}
//   defaultExpandedKeys={['0-0-0']}
//   treeData={treeData}
//   ></DirectoryTree>
// }



export const Note = () => {
  const navigate = useNavigate();

  return (
    <div className={styles.main}>
      <div className="left">
        <LeftMenu />
      </div>
      <div className="right">
        <div className="header font-bold">header</div>
        <div>
          
        </div>
      </div>
    </div>
  );
};
