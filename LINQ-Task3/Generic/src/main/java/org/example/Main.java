package org.example;

public class Main {
    public static void main(String[] args) {
        RandomizedList<Integer> rList = new RandomizedList<>();
        rList.add(10);
        rList.add(20);
        rList.add(30);

        System.out.println(rList.get(2));
        System.out.println(rList.isEmpty());
    }
}