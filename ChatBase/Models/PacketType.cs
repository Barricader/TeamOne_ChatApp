namespace ChatBase.Models {
    public enum PacketType {
        Null,           // Should never happen
        Message,
        ClientID,
        Goodbye,
        RequestAllRooms,
        RequestCreateRoom,
        RequestUser,
        ResponseAllRooms,
        ResponseAllUsers,
        UserJoined,
        RoomCreated
    };
}
