mergeInto(LibraryManager.library, {
  PlayDeckBridge_PostMessage: function (method) {
    const parent = window.parent.window;
    parent.postMessage(
      { playdeck: { method: UTF8ToString(method) } },
      "*"
    );
  },

  PlayDeckBridge_PostMessage_IntValue: function (method, value) {
    const parent = window.parent.window;
    parent.postMessage(
      { playdeck: { method: UTF8ToString(method), value: value } },
      "*"
    );
  },

  PlayDeckBridge_PostMessage_StringValue: function (method, value) {
    const parent = window.parent.window;
    parent.postMessage(
      { playdeck: { method: UTF8ToString(method), value: UTF8ToString(value) } },
      "*"
    );
  },

  PlayDeckBridge_PostMessage_SetData: function (key, value) {
    const parent = window.parent.window;
    parent.postMessage(
      { playdeck: { method: "setData", key: UTF8ToString(key), value: UTF8ToString(value) } },
      "*"
    );
  },

  PlayDeckBridge_PostMessage_GetData: function (key) {
    const parent = window.parent.window;
    parent.postMessage(
      { playdeck: { method: "getData", key: UTF8ToString(key) } },
      "*"
    );
  },

  PlayDeckBridge_PostMessage_RequestPayment: function(data) {
    const parent = window.parent.window;
    const jsonData = JSON.parse(UTF8ToString(data));
    console.log(jsonData);
    parent.postMessage(
      { playdeck: { method: "requestPayment", value: jsonData } },
      "*"
    );
  },

  PlayDeckBridge_PostMessage_GetPaymentInfo: function(data) {
    const parent = window.parent.window;
    const jsonData = JSON.parse(UTF8ToString(data));
    console.log(jsonData);
    parent.postMessage(
      { playdeck: { method: "getPaymentInfo", value: jsonData } },
      "*"
    );
  },

  PlayDeckBridge_PostMessage_CustomShare: function(data) {
    const parent = window.parent.window;
    const jsonData = JSON.parse(UTF8ToString(data));
    console.log(jsonData);
    parent.postMessage(
      { playdeck: { method: "customShare", value: jsonData } },
      "*"
    );
  },

  PlayDeckBridge_PostMessage_GetShareLink: function(data) {
    const parent = window.parent.window;
    const jsonData = JSON.parse(UTF8ToString(data));
    console.log(jsonData);
    parent.postMessage(
      { playdeck: { method: "getShareLink", value: jsonData } },
      "*"
    );
  },

  PlayDeckBridge_PostMessage_OpenTelegramLink: function(data) {
    const parent = window.parent.window;
    const link = UTF8ToString(data);
    console.log(link);
    parent.postMessage(
      { playdeck: { method: "openTelegramLink", value: link } },
      "*"
    );
  },

  PlayDeckBridge_PostMessage_GetPlaydeckState: function() {
    const parent = window.parent.window;
    parent.postMessage(
      { playdeck: { method: "getPlaydeckState" } },
      "*"
    );
  }
});
