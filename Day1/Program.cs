using System.Collections;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
 

static int[,] CreateBox(int m, int n)
{
    //m - sirka
    //n - vyska
    int[,] array = new int[n,m];


    while (m == 0 && n == 0) {
        
        for (int i = 0; i < m; i++) {
            array[0,i] = 1;
        }
        for (int i = 0; i < m; i++) {
            array[n-1,i] = 1;
        }
        for (int i = 0; i < n; i++) {
            array[i,0] = 1;
        }
        for (int i = 0; i < n; i++) {
            array[m-1,i] = 1;
        }
        
        m = 0;
        n = 0;
    }

    return array;
}

int m = 4;
int n = 5;

var box = CreateBox(m, n);

foreach (var x in box) {
  Console.WriteLine(x);
}


