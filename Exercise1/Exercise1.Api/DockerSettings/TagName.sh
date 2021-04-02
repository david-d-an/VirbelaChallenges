#!/bin/bash

acct_name="dong82"
arr=("$@")
for img_name in "${arr[@]}";
do
    repo="${acct_name}/${img_name}"
    uri="https://registry.hub.docker.com/v1/repositories/${repo}/tags"
    data=$(wget -q $uri -O -)

    # echo $data | jq '[.[]|select(.name!="latest")]' | jq '.|=sort_by( (.name | explode | map(-.)) )' | jq -r '.[0:1][].name'
    echo $data | jq -r '[.[]|select(.name!="latest")] | sort_by((.name | explode | map(-.)) ) | .[0:1][].name'
done
