package main

import (
	"fmt"
	"strconv"
)

func print(a ...any) (n int, err error) { return fmt.Print(a...) }

func println(a ...any) (n int, err error) { return fmt.Println(a...) }

func debug(debug bool, a ...any) (n int, err error) {
	if debug {
		return fmt.Println(a...)
	}
	return 0, nil
}

func collate(c chan int) string {
	var res string = ""
	for x, ok := <-c; ok; x, ok = <-c {
		res += strconv.Itoa(x)
	}
	return res
}

func collateN(c chan int, N int) string {
	var res string = ""
	for x, ok := <-c; ok && x < N; x, ok = <-c {
		res += strconv.Itoa(x)
	}
	return res
}

func iterate(c chan int64, N int) {
	count := 0
	for x, ok := <-c; ok && count < N; x, ok = <-c {
		print(x)
		count++
	}
	println("")
}
