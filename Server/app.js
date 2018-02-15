var io = require("socket.io")(process.envPort||3000);

console.log("Server started");
	
var playerCount = 0;
	
io.on("connection", function(socket){
	console.log("Client connected");
	
	socket.broadcast.emit("spawn player");
	playerCount ++;
	
	for(var i = 0; i < playerCount; i++){
		socket.emit("spawn player");
		console.log("Adding a new player");
	}
	
	socket.on("playerhere", function(data){
		console.log("player is logged in!");
	});
	
	socket.on("disconnect", function(){
		console.log("player disconnected");
		playerCount--;
	});
});