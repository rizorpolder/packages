mergeInto(LibraryManager.library, {

  ShowKeyboard: function (sourceInt, initTextInt) {
    var source = UTF8ToString(sourceInt);
    var initText = UTF8ToString(initTextInt);
    var result = prompt("", initText);

    if (result == null || result == "") {
    } else {
      myGameInstance.SendMessage(source, "OnEditComplete", result);
    }
  },

  // in app
  GetCostInternal: function (productIdInt) {
    var productId = UTF8ToString(productIdInt);

    for (const product of gameShop) {
      if (productId == product.id) {
        return parseFloat(product.priceValue);
      }
    }

    return -1;
  },

  GetCurrencyInternal: function (productIdInt) {
    var productId = UTF8ToString(productIdInt);

    for (const product of gameShop) {
      if (productId == product.id) {
        var bufferSize = lengthBytesUTF8(product.priceCurrencyCode) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(product.priceCurrencyCode, buffer, bufferSize);
        return buffer;
      }
    }

    return "";
  },

  IsStoreInitialized: function () {
    if (typeof isPurchasesInitialized !== "undefined" && isPurchasesInitialized != null) {
        return true;
    }

    return false;
  },

  RestorePurchases: function () {
    if (typeof isPurchasesInitialized !== "undefined" && isPurchasesInitialized != null) {
      var purchasesString = "";
      console.log('Yandex: RestorePurchases ' + purchases);
      for (let i = 0; i < purchases.length; i++) {
        purchasesString += purchases[i].productID;
        if (i != purchases.length - 1) {
          purchasesString += ",";
        }
      }

       console.log('Yandex: RestorePurchases 2 ' + purchasesString);

      myGameInstance.SendMessage("YandexStore", "OnRestorePurchases", purchasesString);
    }
  },

  BuyProductInternal: function (productIdInt) {
    var productId = UTF8ToString(productIdInt);

    payments.purchase({ "id": productId }).then(purchase => {
      purchases.push(purchase);
      myGameInstance.SendMessage("YandexStore", "OnPurchaseProduct", productId);
    }).catch((message) => {
      console.log("Yandex: err purchase " + message)
    });
  },

  ConsumePurchaseInternal: function (productIdInt) {
    var productId = UTF8ToString(productIdInt);
    console.log("try consume product " + productId);

    let purchaseIndex = -1;
    for (let i = 0; i < purchases.length; i++) {
      if (purchases[i].productID === productId) {
        console.log("success consume product " + productId);
        purchaseIndex = i;
        payments.consumePurchase(purchases[i].purchaseToken);
        break
      }
    }

    if (purchaseIndex > -1) {
      purchases.splice(purchaseIndex, 1);
    }
  },


  // cloud
  SavePlayerData: function (dataId) {
    var data = UTF8ToString(dataId);
    console.log("Yandex: try save data")
    initPlayer().then(_player => {
      if (_player.getMode() !== 'lite') {
        console.log("Yandex: player is auth, can save")
        player.setData({
          data: data,
        }).then(() => {
          console.log("Yandex: data saved");
        })
      } else {
        console.log("Yandex: player is't auth, can't save")
      }
    });
  },

  LoadPlayerData: function () {
    console.log("Yandex: try load data")

    initPlayer().then(_player => {
      console.log("Yandex: player inited " + _player.getMode())

      if (_player.getMode() !== 'lite') {
        player.getData(["data"]).then(dataDictionary => {
          console.log("Yandex: data loaded")

          if ("data" in dataDictionary) {
            myGameInstance.SendMessage("YandexRemoteDataManager", "OnPlayerDataLoaded", dataDictionary["data"]);
          } else {
            myGameInstance.SendMessage("YandexRemoteDataManager", "OnPlayerDataLoaded", "");
          }

        }).catch(e => {
          console.log("Yandex: err load data" + e)
          myGameInstance.SendMessage("YandexRemoteDataManager", "OnPlayerDataLoaded", "");
        })
      } else {
        myGameInstance.SendMessage("YandexRemoteDataManager", "OnPlayerDataLoaded", "");
      }
    }).catch(e => {
      console.log("Yandex: err load data" + e)
      myGameInstance.SendMessage("YandexRemoteDataManager", "OnPlayerDataLoaded", "");
    })
  },

  // rate app
  RateAppYandex: function () {
    ysdk.feedback.canReview()
      .then(({ value, reason }) => {
        if (value) {
          ysdk.feedback.requestReview()
            .then(({ feedbackSent }) => {
              console.log(feedbackSent);
            })
        } else {
          console.log(reason)
        }
      })
  },

   GetLanguageCodeFromUrl: function ()
 {
  //var urlParams = new URLSearchParams(window.location.search);
    //var language = urlParams.get('lang');

    var language = ysdk.environment.i18n.lang;
    if (!language)
        return null;

    // Allocate memory for the string in Unity's heap
    var bufferSize = lengthBytesUTF8(language) + 1;
    var buffer = Module._malloc(bufferSize);
    stringToUTF8(language, buffer, bufferSize);

    // Return a pointer to the memory containing the string
    return buffer;
  },

  // authorize
  AuthorizeInternal: function () {
    try {
      ysdk.getPlayer({ signed: true }).then(_player => {
		var pMode = _player.getMode();
        if (pMode !== 'lite') {
          myGameInstance.SendMessage("Context/YandexAuthorizer", "OnAuthorized", _player.getUniqueID());
        }else{
			console.log("Auth pMode:" + pMode);
			myGameInstance.SendMessage("Context/YandexAuthorizer", "OnAuthorized", "");
		}
      }).catch(err => {
        myGameInstance.SendMessage("Context/YandexAuthorizer", "OnAuthorized", "");
      });
    } catch (error) {
      // ignore
    }
  },

  ShowLoginWindowInternal: function() {
    try {
      ysdk.getPlayer({ signed: true }).then(_player => {
        if (_player.getMode() === 'lite') {
          ysdk.auth.openAuthDialog().then(() => {
            myGameInstance.SendMessage("Context/YandexAuthorizer", "OnSuccessLogIn");
          });
        }
      });
    } catch (error) {
      // ignore
    }
  },

	YandexMetricaGoal: function(goalInt)
	{
		var goal = UTF8ToString(goalInt);
		yaMetricaGoal(goal);
	},
});
