#include <WinSock2.h>
#include <stdio.h>

#pragma comment(lib, "ws2_32.lib")

int main()
{
	// initial socket library
	WORD wVerisonRequested;
	WSADATA wsaData;
	int err;
	wVerisonRequested = MAKEWORD(1, 1);
	err = WSAStartup(wVerisonRequested, &wsaData);
	if (err != 0)
	{
		return -1;
	}
	//if (LOBYTE(wsaData.wVersion) != 1 ||
	// HIBYTE(wsaData.wHighVersion) != 1)
	//{
	// WSACleanup();
	// return -1;
	//}

	// create socket
	SOCKET sockClient = socket(AF_INET, SOCK_STREAM, 0);

	// connect server socket
	SOCKADDR_IN addrServer;
	addrServer.sin_addr.S_un.S_addr = inet_addr("127.0.0.1");
	addrServer.sin_family = AF_INET;
	addrServer.sin_port = htons(6000);
	connect(sockClient, (SOCKADDR *)&addrServer, sizeof(addrServer));

	// receive
	char recvBuf[100];
	recv(sockClient, recvBuf, 100, 0);

	//
	printf("%s", recvBuf);

	// send
	char sendBuf[100] = "this is lisi\n";
	send(sockClient, sendBuf, strlen(sendBuf) + 1, 0);

	// close connection socket
	closesocket(sockClient);

	// clean socket library
	WSACleanup();
}