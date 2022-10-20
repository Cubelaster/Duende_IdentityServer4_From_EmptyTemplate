import React, { useCallback, useMemo } from "react";
import { useEffect } from "react";
import { UserManager } from "oidc-client";
import { Fragment } from "react";

const Dashboard = () => {
  const config = useMemo(
    () => ({
      authority: "https://localhost:5001",
      client_id: "react",
      redirect_uri: "https://localhost:5005/callback",
      response_type: "code",
      scope: "openid profile testscope",
      post_logout_redirect_uri: "https://localhost:5005/",
    }),
    []
  );

  const mgr = useMemo(() => new UserManager(config), [config]);

  const log = (...params: any[]): void => {
    document.getElementById("results")!.innerText = "";

    Array.prototype.forEach.call(params, function (msg) {
      if (typeof msg !== "undefined") {
        if (msg instanceof Error) {
          msg = "Error: " + msg.message;
        } else if (typeof msg !== "string") {
          msg = JSON.stringify(msg, null, 2);
        }
        document.getElementById("results")!.innerText += msg + "\r\n";
      }
    });
  };

  const login = useCallback((): void => {
    mgr.signinRedirect();
  }, [mgr]);

  const api = useCallback((): void => {
    mgr.getUser().then(function (user) {
      var url = "https://localhost:5002/api/Identity";

      var xhr = new XMLHttpRequest();
      xhr.open("GET", url);
      xhr.onload = function () {
        log(xhr.status, JSON.parse(xhr.responseText));
      };
      xhr.setRequestHeader("Authorization", "Bearer " + user!.access_token);
      xhr.send();
    });
  }, [mgr]);

  useEffect(() => {}, []);

  const logout = useCallback((): void => {
    mgr.signoutRedirect();
  }, [mgr]);

  useEffect(() => {
    document.getElementById("login")!.addEventListener("click", login, false);
    document.getElementById("api")!.addEventListener("click", api, false);
    document.getElementById("logout")!.addEventListener("click", logout, false);

    mgr!.events.addUserSignedOut(function () {
      log("User signed out of IdentityServer");
    });

    mgr!.getUser().then(function (user) {
      if (user) {
        log("User logged in", user.profile);
      } else {
        log("User not logged in");
      }
    });
  }, [login, logout, api, mgr]);

  return (
    <Fragment>
      <h2>Dashboard</h2>

      <button id="login">Login</button>
      <button id="api">Call API</button>
      <button id="logout">Logout</button>

      <pre id="results"></pre>
    </Fragment>
  );
};

export default Dashboard;
