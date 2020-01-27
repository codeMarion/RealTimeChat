import React, {useEffect, useState} from 'react';
import * as signalR from '@aspnet/signalr';
const App: React.FC = () => {

  const [messages, setMessages] = useState<{name:string, message:string}[]>([]);
  const [hubconn, sethubconn] = useState<signalR.HubConnection>();
  const [text, setText] = useState('')
  const [myName, setMyName] = useState();

  useEffect(() => {
    setMyName(window.prompt("Name","Marion"))
    const conn = new signalR.HubConnectionBuilder()
    .withUrl('https://localhost:44344/chat')
    .build();

    conn.start();
    
    sethubconn(conn);
    conn.on("SendToAll", (n, message) => {
      setMessages(messages => [...messages,{name: n, message: message}]);
    })

  },[])

  function sendmess() {
    hubconn?.invoke("SendToAll", myName, text);
  }

  return (
    <div>
      <p>Hi, {myName}</p>
      <input type="text" onChange={(e) => setText(e.target.value)}/>
      <button onClick={sendmess}>Send</button>
      {messages.map((mess, i) => (
        <p key={i}><strong>{mess.name}: </strong>{mess.message}</p>
      ))}
    </div>
  );
}

export default App;
