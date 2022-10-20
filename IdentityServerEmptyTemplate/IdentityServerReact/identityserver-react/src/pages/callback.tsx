import { UserManager } from "oidc-client";
import { useEffect } from "react";
import { BrowserRouter as Router, useNavigate } from "react-router-dom";

const Callback = () => {
  const navigate = useNavigate();
  useEffect(() => {
    console.log("Ušo u callback");
    new UserManager({ response_mode: "query" })
      .signinRedirectCallback()
      .then(function () {
        navigate("/");
      })
      .catch(function (e) {
        console.error(e);
      });
  }, []);
  return <h2>Callback</h2>;
};

export default Callback;
