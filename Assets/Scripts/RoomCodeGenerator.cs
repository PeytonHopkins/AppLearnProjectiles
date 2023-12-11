using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RoomCodeGenerator
{
    static string alphaNumeric = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
    public static string GenerateRoomCode()
    {
        string code = "";
        for (int i = 0; i < 4; i++)
        {
            int charIndex = Random.Range(0,alphaNumeric.Length -1);
            code += alphaNumeric[charIndex];
        }
        return code;
    }

}
