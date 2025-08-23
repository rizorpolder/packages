mergeInto(LibraryManager.library, {
  IsWebView: function () {
       if(bridge.isWebView()){
		return 1;
       }else{
		return 0;
       }
  },

  ShowKeyboard: function (sourceInt, initTextInt) {

  },

  // In app
  RestorePurchases: function () {
  },

  BuyProductInternal: function (productIdInt, nameInt, descriptionInt, priceInt) {
     var productId = UTF8ToString(productIdInt);
     console.log("[iap] buy product internal " + productId);

     bridge.send('VKWebAppShowOrderBox',
       {
         type: 'item',
         item: productId,
       })
       .then( (data) => {
                console.log('Покупка состоялась.', data);
                myGameInstance.SendMessage("VkStore", "OnPurchaseProduct", productId);
            })
       .catch( (e) => {console.log('Ошибка!', e) });
  },


  // Social
  RateApp: function () {
  },

  ShowWallPostBox: function (messagePtr, gameLinkPtr, picturePtr) {
    var message = UTF8ToString(messagePtr);
    var gameLink = UTF8ToString(gameLinkPtr);
    var pictureUrl = UTF8ToString(picturePtr);
    console.log(pictureUrl);

	bridge.send('VKWebAppShowStoryBox', {
	  "background_type": "image",
	  "url": pictureUrl,
	  "locked": true,
	  "attachment": {
		"type": "url",
		"text": "game",
		"url": gameLink
	  },
	   "stickers": [{
		  "sticker_type": "native",
		  "sticker": {
			"action_type": "text",
			"action": {
			  "text": message,
			  "style": "lobster",
			  "background_style": "none",
			  "selection_color": "#000000"
			},
			"transform": {
			  "gravity": "center_top",
			  "translation_y": 0.1
			}
		  }
		}]
	  })
	  .then( (data) => {
		myGameInstance.SendMessage("WallPost", "OnPostSuccess");
	  })
	  .catch( (e) => {
		console.log(e);
		myGameInstance.SendMessage("WallPost", "OnPostFailed");
	  })
  },

  InviteFriends: function (sourceText) {
	bridge.send('VKWebAppShowInviteBox', {
		requestKey: "key-12345" //  Ключ приглашения
	})
  .then( (data) => {
    if (data.success) {
      // Пользователь нажал «Пригласить»
      // ...

      // Этим выбранным пользователям
      // не удалось отправить приглашения
      console.log('Приглашения не отправлены', data.notSentIds);
    }
  })
  .catch( (error) => {
    console.log(error); // Ошибка
  });
  },

  // Authorize
  AuthorizeInternal: function () {
    bridge.send('VKWebAppGetUserInfo', {})
      .then((data) => {
        if (data.id) {
          console.log("vk player " + data.id);
          myGameInstance.SendMessage("Context/VkAuthorizer", "OnAuthorized", data.id.toString());
        }
      })
      .catch((error) => {
        // Ошибка
        console.log(error);
      });
  },

  ShowLoginWindowInternal: function () {
  },

  //VKBridgeFocus
  VKAppFocusSubscribe: function (){
   bridge.subscribe((e)=>{
    const { type, data } = e.detail;
         switch(type) {
           case 'VKWebAppViewHide':
             myGameInstance.SendMessage("AdsManager", "VKBridgeAppFocusOut");
             console.log("Bridge Focus false");
             break;
           case 'VKWebAppViewRestore':
             myGameInstance.SendMessage("AdsManager", "VKBridgeAppFocusIn");
             console.log("Bridge Focus true");
             break;
         }
    });
  },

});