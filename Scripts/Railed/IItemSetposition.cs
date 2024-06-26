public interface IItemSetposition //아이템 또는 자원을 둘 때 정수 위치로 만드는 함수
{
    void SetPosition();
}

public interface IItemCanStack //내려놓은 아이템을 쌓을 수 있는 기능
{
    void CheckTopObject();
    void CanStack();
}