

const getHeaders = () => {
    // 创建Headers对象
const headers = new Headers();

// 添加请求头
headers.append('Content-Type', 'application/json');
headers.append('User-Agent', 'PostmanRuntime/7.32.3');
headers.append('Accept', '*/*');
headers.append('Accept-Encoding', 'gzip, deflate, br');
headers.append('Connection', 'keep-alive');

const token = window.localStorage.getItem("lucasNote.Token");
headers.append('Authorization', 'Bearer ' + token);
return headers;
}
export default getHeaders;