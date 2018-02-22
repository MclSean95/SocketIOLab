var io = require("socket.io")(process.envPort||3000);
var shortid = require("shortid");

console.log("Server started");
	
var players = [];	

io.on("connection", function(socket){
	var thisPlayerID = shortid.generate();
	
	players.push(thisPlayerID);
	
	console.log("Client connected spawning player id:", thisPlayerID);
	
	socket.broadcast.emit("spawn player", {id: thisPlayerID});

	players.forEach(function(playerId){
		if(playerId == thisPlayerID) return;
		
		socket.emit("spawn player", {id: playerId});
		console.log("Adding a new player", playerId);
	});
	
	socket.on("move", function(data){
		data.id = thisPlayerID;
		console.log("player position is: ", JSON.stringify(data));
		socket.broadcast.emit("move", data);
	});
	
	socket.on("disconnect", function(){
		console.log("player disconnected");
		players.splice(players.indexOf(thisPlayerID), 1);
		socket.broadcast.emit("disconnected", {id: thisPlayerID});
	});
	
});